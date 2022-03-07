using System;
using System.Collections.Generic;

namespace WebCrawlerSWD.ConstURL
{
    public class WebURL
    {
        public readonly string ViecLamIT = "https://www.topcv.vn/tim-viec-lam-it-phan-mem-c10026";

        public readonly string IT_ChuaCoKinhNghiem = "https://www.topcv.vn/tim-viec-lam-it-phan-mem-c10026?exp=1&sort=up_top";

        public readonly string IT_Duoi1Nam = "https://www.topcv.vn/tim-viec-lam-it-phan-mem-c10026?exp=2&sort=up_top";

        public readonly string IT_1Nam = "https://www.topcv.vn/tim-viec-lam-it-phan-mem-c10026?exp=2&sort=up_top";

        public readonly string IT_2Nam = "https://www.topcv.vn/tim-viec-lam-it-phan-mem-c10026?exp=4&sort=up_top";

        public readonly string IT_3Nam = "https://www.topcv.vn/tim-viec-lam-it-phan-mem-c10026?exp=5&sort=up_top";

        public readonly string IT_4Nam = "https://www.topcv.vn/tim-viec-lam-it-phan-mem-c10026?exp=6&sort=up_top";

        public readonly string IT_5Nam = "https://www.topcv.vn/tim-viec-lam-it-phan-mem-c10026?exp=7&sort=up_top";

        public readonly string IT_Tren5Nam = "https://www.topcv.vn/tim-viec-lam-it-phan-mem-c10026?exp=8&sort=up_top";

        public List<String> ListURLs;
        
        public WebURL()
        {
            ListURLs = new List<String>();

            ListURLs.Add(ViecLamIT);
            ListURLs.Add(IT_ChuaCoKinhNghiem);
            ListURLs.Add(IT_Duoi1Nam);
            ListURLs.Add(IT_1Nam);
            ListURLs.Add(IT_2Nam);
            ListURLs.Add(IT_3Nam);
            ListURLs.Add(IT_4Nam);
            ListURLs.Add(IT_5Nam);
            ListURLs.Add(IT_Tren5Nam);

        }

        public List<String> GetUrlLinks()
        {
            return ListURLs;
        }

    }
}
