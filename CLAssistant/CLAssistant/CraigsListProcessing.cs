using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace CLAssistant
{
    public partial class MainWindow
    {
        int maxPrice = 0;
        bool hasMaxPrice = false;

        void GetCraigsListResult(string keyword)
        {
            GetCraigsListResult(keyword, string.Empty);
        }

        /// <summary>
        /// Sends a query to CraigsList.  Assumes that validation for input has already been done.
        /// </summary>
        void GetCraigsListResult(string keyword, string maxPrice)
        {
            // Set up default query
            string query = string.Format("query={0}", keyword);

            // Include asking for maxprice
            if (maxPrice != string.Empty)
            {
                //format = rss &
                   query = string.Format("{0}&max_price={1}", query, maxPrice);
                this.maxPrice = Convert.ToInt32(maxPrice);
                hasMaxPrice = true;
            }

            string inputURL = string.Format("https://seattle.craigslist.org/search/sss?{0}", query);
            string result = string.Empty;

            ResultsTextbox.Clear();
            ResultsTextbox.AppendText("Loading");

            worker.DoWork += delegate(Object s, DoWorkEventArgs args)
            {
                result = GetWebPage(inputURL);
            };

            worker.ProgressChanged += delegate(object s, ProgressChangedEventArgs args)
            {
                ShowProgress(args.ProgressPercentage);
            };

            worker.RunWorkerCompleted += delegate(Object s, RunWorkerCompletedEventArgs args)
            {
                if (!isCancelled)
                {
                    ParseCraigsListResult(result);
                }
            };

            worker.RunWorkerAsync();
        }

        void SaveCraigsListResult(CraigsListResult parsedRow)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CraigsListResult));
            TextWriter writer = new StreamWriter(LOG_FILE_NAME, true);
            serializer.Serialize(writer, parsedRow);
            writer.Close();
        }

        void ClearPreviousCraigsListResult()
        {
            TextWriter writer = new StreamWriter(LOG_FILE_NAME);
            writer.Close();
        }

        string GetLatestCraigsListResult()
        {
            if (!(File.Exists(LOG_FILE_NAME)))
            {
                return string.Empty;
            }
            TextReader reader = new StreamReader(LOG_FILE_NAME);
            string latestCraigsListResult = reader.ReadLine();
            reader.Close();
            return latestCraigsListResult;
        }

        CraigsListResultCollection GetAllPreviousCraigsListResult()
        {
            if (!(File.Exists(LOG_FILE_NAME)))
            {
                return null;
            }
            XmlSerializer serializer = new XmlSerializer(typeof(CraigsListResultCollection));
            StreamReader reader = new StreamReader(LOG_FILE_NAME);
            CraigsListResultCollection previousCraigsListResults = (CraigsListResultCollection)serializer.Deserialize(reader);
            reader.Close();
            return previousCraigsListResults;
        }

        void ParseCraigsListResult(string result)
        {
            ResultsTextbox.Clear();
            List<CraigsListResult> newResults = new List<CraigsListResult>();
            CraigsListResultCollection previousCraigsListResults = GetAllPreviousCraigsListResult();
            CraigsListResult latestCraigsListResult = null;
            //if (null != previousCraigsListResults)
            //{
            //    latestCraigsListResult = previousCraigsListResults.CraigsListResult[0];
            //}

            //ClearPreviousCraigsListResult();

            string rowPattern = "<li class=\"result-row\"";
            string[] rows = Regex.Split(result, rowPattern);

            string spanPattern = "<p class=\"result-info\">";
            foreach (string row in rows)
            {
                if (row.Contains(spanPattern))
                {
                    string temp = "<li " + row.Trim();
                    // if row doesn't end with </li> clear it
                    if(!temp.EndsWith("</li>"))
                    {
                        temp = temp.Substring(0, temp.IndexOf("</li>") + 5);
                    }


                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(temp);
                    CraigsListResult parsedRow = ParseCraigsListRow(xml);

                    if (parsedRow == latestCraigsListResult)
                    {
                        ResultsTextbox.AppendText("No more new results were found.");
                        break;
                    }

                    int setPrice = 0;
                    try
                    {
                        setPrice = Convert.ToInt32(parsedRow.price);
                    }
                    catch (Exception)
                    {
                        setPrice = -1;
                    }

                    if (maxPrice > setPrice)
                    {
                        ShowCraigsListParsedRow(parsedRow);
                        newResults.Add(parsedRow);
                    }
                }
            }

            //SaveCraigsListResult(previousCraigsListResults);
        }

        void ShowCraigsListParsedRow(CraigsListResult parsedRow)
        {
            string result = string.Empty;

            // See if sale posting includes picture
            result += (parsedRow.hasPicture) ? "Sale posting includes picture\r\n" : "Sale posting does *NOT* include picture\r\n";

            // Show the date of posting
            result += string.Format("Date of posting is: {0}\r\n", parsedRow.date);

            // Show price of posting
            result += string.Format("Posted price: {0}\r\n", parsedRow.price);

            // Show description
            result += string.Format("Title of posting: {0}\r\n", parsedRow.description);

            // Show hyperlink
            result += string.Format("Link to posting: {0}\r\n", parsedRow.hyperlink);

            // Show location
            result += string.Format("Location of item: {0}\r\n", parsedRow.location);

            // Add a blank line
            result += "\r\n";

            ResultsTextbox.AppendText(result);
        }

        CraigsListResult ParseCraigsListRow(XmlDocument row)
        {
            string date = null;
            string hyperlink = null;
            string description = null;
            string price = null;
            string location = null;
            bool hasPicture = false;

            XmlNode picNode = row.SelectSingleNode("//li/a");
            hasPicture = picNode.Attributes["class"].Value.Equals("result-image gallery");

            XmlNode hRefNode = row.SelectSingleNode("//li/p/a");
            hyperlink = "http://seattle.craigslist.org" + hRefNode.Attributes["href"].Value;
            description = hRefNode.InnerText;

            XmlNode dateNode = row.SelectSingleNode("//li/p/time");
            date = dateNode.Attributes["title"].Value;

            XmlNode result = row.SelectSingleNode("//li/p/span[@class='result-meta']");
            foreach(XmlNode node in result.ChildNodes) {
                if (node.Attributes["class"] != null)
                {
                    switch (node.Attributes["class"].Value)
                    {
                        case "result-price":
                            price = node.InnerText;
                            break;
                        case "result-hood":
                            location = node.InnerText;
                            break;
                    }
                }
            }

            return new CraigsListResult
            {
                date = date,
                hyperlink = hyperlink,
                description = description,
                price = price,
                location = location,
                hasPicture = hasPicture
            };
        }

        //CraigsListResult ParseCraigsListRow(string row)
        //{
        //    string input = row;
        //    input = input.Replace("\r", string.Empty);
        //    input = input.Replace("\n", string.Empty);
        //    input = input.Replace("\t", string.Empty);

        //    // Determine whether there's a picture
        //    string pictureSpanPattern = "class=\"result-image gallery\"";
        //    bool hasPicture = (input.Contains(pictureSpanPattern)) ? true : false;

        //    // Determine whether there's a price
        //    string priceSpanPattern = "<span class=\"result-price\">";
        //    bool hasPrice = (input.Contains(priceSpanPattern)) ? true : false;
        //    int elementOffset = (hasPrice) ? 2 : 0;

        //    string splitPattern = "{{mySplitPattern}}";

        //    string endSpanPattern = "</span>";
        //    input = input.Replace(endSpanPattern, splitPattern);

        //    string hyperlinkPattern = "<a href=";
        //    input = input.Replace(hyperlinkPattern, splitPattern);

        //    string endHyperlinkPattern = "</a>";
        //    input = input.Replace(endHyperlinkPattern, splitPattern);

        //    string fontSizePattern = "<font size=\"-1\">";
        //    input = input.Replace(fontSizePattern, splitPattern);

        //    string endFontSizePattern = "</font> <small class=\"gc\">";
        //    input = input.Replace(endFontSizePattern, splitPattern);

        //    string endTagBracketPattern = ">";
        //    input = input.Replace(endTagBracketPattern, splitPattern);

        //    string[] pieces = Regex.Split(input, splitPattern);

        //    string date = pieces[8 + elementOffset];
        //    string hyperlink = pieces[10 + elementOffset];
        //    hyperlink = hyperlink.Replace("\"", string.Empty).Trim();
        //    hyperlink = "http://seattle.craigslist.org" + hyperlink;
        //    string description = pieces[11 + elementOffset];
        //    description = description.Replace("-", string.Empty).Trim();
        //    description = description.Replace("?", string.Empty).Trim();
        //    string price = (hasPrice) ? pieces[4] : "N/A";
        //    price = price.Replace("&#x0024;", string.Empty).Trim();
        //    string location = pieces[18 + elementOffset];
        //    location = location.Replace("</small", string.Empty).Trim();
        //    location = location.Replace("<span class=\"p\"", string.Empty).Trim();
        //    location = location.Replace("(", string.Empty).Trim();
        //    location = location.Replace(")", string.Empty).Trim();
        //    location = (location == string.Empty) ? "Unspecified" : location;

        //    return new CraigsListResult
        //    {
        //        date = date,
        //        hyperlink = hyperlink,
        //        description = description,
        //        price = price,
        //        location = location,
        //        hasPicture = hasPicture
        //    };
        //}
    }
}
