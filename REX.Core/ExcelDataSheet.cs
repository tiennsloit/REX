using System;
using System.Collections.Generic;
using System.Text;

namespace REX.Core
{
    public class ExcelDataSheet
    {
        private string name { get; set; }
        private object data { get; set; }
        private Dictionary<string, object> dictionaryData { get; set; }

        public ExcelDataSheet(string name, object data)
        {
            this.name = name;
            this.data = data;
        }

        public ExcelDataSheet(string name, Dictionary<string, object> dictionaryData)
        {
            this.name = name;
            this.dictionaryData = dictionaryData;
        }

        public string Name { get { return this.name; } }
        public object Data { get { return this.data; } }
        public Dictionary<string, object> DictionaryData { get { return this.dictionaryData; } }
    }
}
