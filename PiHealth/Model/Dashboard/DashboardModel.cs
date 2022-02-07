using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiHealth.Web.Model.Dashboard
{
    public class DashboardModel
    {
        public DashboardModel()
        {
            this.sanplesChart = new DashboardSamplesCharModel();
        }
        public int totalPatients { get; set; }
        public int todayPatients { get; set; }
        public int todaySamples { get; set; }
        public int todayReports { get; set; }
        public int pedningSamples { get; set; }
        public int sampleInProgress { get; set; }

        public double todayExpectedCollection { get; set; }
        public double todayActualCollection { get; set; }
        public int reportDeleveredtoday { get; set; }
        public DashboardSamplesCharModel sanplesChart { get; set; }
    }

    public class DashboardSamplesCharModel
    {
        public DashboardSamplesCharModel()
        {
            this.dates = new List<string>();
            this.collected = new List<int>();
            this.reported = new List<int>();
            this.collection = new List<double>();
        }

        public List<string> dates { get; set; }
        public List<int> collected { get; set; }
        public List<int> reported { get; set; }
        public List<double> collection { get; set; }
    }
}
