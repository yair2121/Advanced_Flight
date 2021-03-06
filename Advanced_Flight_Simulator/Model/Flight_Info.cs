using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Linq;
namespace Advanced_Flight_Simulator
{
    /*
     * Handle Flight information- support different queries to extract data from specific attribute or specifiec row.
     * Require csv(for data) and xml(for headers) paths.
     */
    public class Flight_Info
    {
        private List<Attribute> attributes;
        private List<Dictionary<string, string>> rows; // Keep rows in list- each row is a dictionary- key is attribute name.
        public Flight_Info()
        {
            rows = new List<Dictionary<string, string>>();
            attributes = new List<Attribute>();
        }
        /*
         * Extract data from csv and xml files using thier paths.
         */
        public void init_Flight_Info(string csv_path)
        {
            extract_headers("playback_small.xml");
            extract_values(csv_path);
        }
        /*
        * Extract Headers from xml file. Headers are stored in "output"-name.
        */
        private void extract_headers(string xml_path)
        {
            XElement Xelement = XElement.Load(xml_path);
            XDocument xDoc = XDocument.Load(xml_path);
            IEnumerable<string> query = Xelement.Descendants("output").Descendants("name").Select(name => (string)name);
            foreach (var name in query.ToList())
            {
                attributes.Add(new Attribute(name));
            }
        }
        /*
         * Extract values into attributes and rows from a path to a CSV file.
         */
        private void extract_values(string csv_path)
        {
            try
            {
                string[] lines = File.ReadAllLines(csv_path);
                lines = lines.ToArray();
                int coulumn_index = 0;
                int row_index = 0;
                string current_value;
                string current_name;
                foreach (var line in lines)
                {
                    rows.Add(new Dictionary<string, string>());
                    string[] current_line = line.Split(',');
                    foreach (var attribute in attributes)
                    {
                        current_name = attribute.name;
                        if (rows[row_index].ContainsKey(attribute.name))
                        {
                            current_name += "2";
                        }; // Add 2 to name if two attributes have the same name
                        current_value = current_line[coulumn_index].ToString();
                        attribute.add_value(current_value);
                        rows[row_index].Add(current_name, current_value);
                        coulumn_index++;
                    }
                    coulumn_index = 0;
                    row_index++;
                }
            }
            catch (ArgumentException) { }
        }

        /*
         * Return a list of all the values in given attribute.
         */
        public List<string> get_attribute(string att_name)
        {
            foreach (var attribute in this.attributes)
            {
                if (attribute.name == att_name)
                {
                    return attribute.get_values();
                }
            }
            return new List<string>();
        }
        /*
        * Return the row of given row.
        */
        public Dictionary<string, string> get_row(int row)
        {
            return this.rows[row];
        }
        /*
        * Return the value of the given attribute in given row.
        */
        public string get_value(int row, string attribute_name)
        {
            if (!String.IsNullOrEmpty(attribute_name))
            {
                if (row < row_count() && rows.First().ContainsKey(attribute_name))
                {
                    return rows[row][attribute_name];
                }
            }
            return string.Empty;

        }

        /*
         * Convert given row values into a single string wheres each value is seperated by ",".
         */
        public string get_row_string(int row)
        {
            return string.Join(",", rows[row].Select(x => x.Value).ToArray()) + "\r\n";
        }
        /*
        * Return the number of the rows.
        */
        public int row_count()
        {
            return this.rows.Count() - 1;
        }
        /*
        * Return the number of attributes.
        */
        public int attribute_count()
        {
            return this.attributes.Count();
        }
        /*
        * Return list of names of atrributes.
        */
        public List<string> get_attribute_names()
        {
            List<string> names = new List<string>();
            foreach (var attribue in rows.First())
            {
                names.Add(attribue.Key);
            }
            return names;
        }
        /*
        * Return the index of given attribute name. 
        */
        public int getIndex(string name)
        {
            int i = 0;
            foreach (var attribute in rows.First())
            {
                if (name.Equals(attribute.Key))
                {
                    return i;
                }
                i++;
            }
            return -1;
        }
        /*
        * Return attribute name of given index.
        */
        public string getAttributeFromIndex(int index)
        {
            if (index == -1)
            {
                return String.Empty;
            }
            return rows.First().ElementAt(index).Key;
        }
    }
}
