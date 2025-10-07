using System;
using System.Linq;
using System.Windows.Forms;

namespace CSharpEgitimKampi301.EFProject
{
    public partial class FrmStatistics : Form
    {
        public FrmStatistics()
        {
            InitializeComponent();
        }

        EgitimKampiEfTravelDbEntities db = new EgitimKampiEfTravelDbEntities();

        private void FrmStatistics_Load(object sender, EventArgs e)
        {
            lblLocationCount.Text = db.Location.Count().ToString();
            lblSumCapacity.Text = db.Location.Sum(x => (int?)x.Capacity)?.ToString() ?? "0";
            lblGuideCount.Text = db.Guide.Count().ToString();
            lblAvgCapacity.Text = db.Location.Average(x => (double?)x.Capacity)?.ToString("0.00") ?? "0.00";
            lblAvgLocationPrice.Text = db.Location.Average(x => (decimal?)x.Price)?.ToString("0.00") + " ₺";

            int? lastLocationId = db.Location.Max(x => (int?)x.LocationId);
            lblLastCountryName.Text = db.Location
                                        .Where(x => x.LocationId == lastLocationId)
                                        .Select(y => y.Country)
                                        .FirstOrDefault() ?? "-";

            lblCappodociaLocationCapacity.Text = db.Location
                                                    .Where(x => x.City == "Kapadokya")
                                                    .Select(y => (int?)y.Capacity)
                                                    .FirstOrDefault()?.ToString() ?? "0";

            lblTurkiyeCapacityAvg.Text = db.Location
                                            .Where(x => x.Country == "Türkiye")
                                            .Average(y => (double?)y.Capacity)?.ToString("0.00") ?? "0.00";

            var romeGuideId = db.Location
                                .Where(x => x.City == "Roma Turistik")
                                .Select(y => (int?)y.GuideId)
                                .FirstOrDefault();

            lblRomeGuideName.Text = db.Guide
                                      .Where(x => x.GuideId == romeGuideId)
                                      .Select(y => y.GuideName + " " + y.GuideSurname)
                                      .FirstOrDefault() ?? "-";

            var maxCapacity = db.Location.Max(x => (int?)x.Capacity);
            lblMaxCapacityLocation.Text = db.Location
                                           .Where(x => x.Capacity == maxCapacity)
                                           .Select(y => y.City)
                                           .FirstOrDefault() ?? "-";

            var maxPrice = db.Location.Max(x => (decimal?)x.Price);
            lblMaxPriceLocation.Text = db.Location
                                        .Where(x => x.Price == maxPrice)
                                        .Select(y => y.City)
                                        .FirstOrDefault() ?? "-";

            lblEmreGezginLocationCount.Text = db.Location.Count().ToString();
        }
    }
}
