
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

    [Database]
    public class OPM
    {
        public FRT SHP { get; set; }
        public FRT CNE { get; set; }
        public FRT ACC { get; set; }
        public FRT CRR { get; set; }

        public int OPMID { get; set; }
        public string REFNO { get; set; }
        public DateTime? EXD { get; set; }
        public string ROT { get; set; }
        public string MOT { get; set; }
        public string ORG { get; set; }
        public string DST { get; set; }
        public string POU { get; set; }
        public int SHPID { get; set; }
        public int CNEID { get; set; }
        public int ACCID { get; set; }
        public int CRRID { get; set; }
        public string NSTU { get; set; }
        public string PSTU { get; set; }
        public DateTime? ETD { get; set; }
        public DateTime? ATD { get; set; }
        public DateTime? ETA { get; set; }
        public DateTime? ATA { get; set; }
        public DateTime? RETD { get; set; }
        public DateTime? RETA { get; set; }
        public DateTime? ACOT { get; set; }
        public DateTime? TPAD { get; set; }
        public DateTime? TPDD { get; set; }
        public DateTime? ROH { get; set; }
        public DateTime? RTD { get; set; }
        public string VHC { get; set; }
        public string INF { get; set; }
        public string HNDINF { get; set; }
        public string CNTNOS { get; set; }
        public string SEALNOS { get; set; }
        public string PINFOS { get; set; }
    }

    [Database]
    public class OPH
    {
        public OPM OPM { get; set; }
        public FRT SHP { get; set; }
        public FRT CNE { get; set; }
        public FRT ACC { get; set; }
        public FRT MNF { get; set; }
        public FRT NFY { get; set; }
        public FRT CRR { get; set; }

        public int OPHID { get; set; }
        public int OPMID { get; set; }
        public string REFNO { get; set; }
        public DateTime? EXD { get; set; }
        public string ROT { get; set; }
        public string MOT { get; set; }
        public string ORG { get; set; }
        public string DST { get; set; }
        public int SHPID { get; set; }
        public int CNEID { get; set; }
        public int ACCID { get; set; }
        public int MNFID { get; set; }
        public int NFYID { get; set; }
        public int CRRID { get; set; }
        public string NSTU { get; set; }
        public string PSTU { get; set; }
        public string DTM { get; set; }
        public string PTM { get; set; }
        public int NOP { get; set; }
        public decimal GRW { get; set; }
        public decimal VM3 { get; set; }
        public int CHW { get; set; }
        public DateTime? NSTUTS { get; set; }
        public DateTime? PSTUTS { get; set; }
        public DateTime? ROH { get; set; }
        public DateTime? ROS { get; set; }
        public DateTime? EOH { get; set; }
        public DateTime? REOH { get; set; }
        public DateTime? AOH { get; set; }
        public DateTime? AOC { get; set; }
        public DateTime? RTR { get; set; }
        public DateTime? RTD { get; set; }
        public DateTime? POD { get; set; }
        public string PODINF { get; set; }
        public DateTime? DRBD { get; set; }
        public DateTime? DRCD { get; set; }
        public string DDT { get; set; }
        public string CABW { get; set; }
        public string CUSLOC { get; set; }
        public string CNTNOS { get; set; }
        public string OTHINF { get; set; }
    }
}