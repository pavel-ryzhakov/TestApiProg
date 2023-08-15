using System.Diagnostics;

namespace TestApiProg.Dtos
{
    public class GetProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public string ProductLife
        {
            get
            {
                return ProductLifeIn(Date);
            }
            set
            {
                ProductLife = value;
            }

        }

        public string ProductLifeIn(DateTime date)
        {
            var date1 = date;
            DateTime date2 = DateTime.Now;
            var dateres = date2 - date1;

            int min = dateres.Minutes;

            if (min < 2) { return "Разница менее 2 мин"; }

            if (min < 5 & min >= 2) { return  "Разница менее 5 мин"; }

            if (min >= 5) { return "Разница более 5 мин"; }

            return "";
        }


    }
}
