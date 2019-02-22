
using System;
using Starcounter;

namespace DBTM
{

    [Database]
    public class FRT
    {
        public int FRTID { get; set; }
        public string ADN { get; set; }
        public string AD { get; set; }
        public string LOCID { get; set; }
        public string PWD { get; set; }
    }
}