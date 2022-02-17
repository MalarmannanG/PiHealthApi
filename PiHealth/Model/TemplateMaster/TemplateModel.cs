using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiHealth.Web.Model.TemplateMaster
{
    public class TemplateMasterModel
    {
        public TemplateMasterModel()
        {
            templatePrescriptionModel = new List<TemplatePrescriptionModel>();
        }
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string compliants { get; set; }
        public string examination { get; set; }
        public string impression { get; set; }
        public string advice { get; set; }
        public string plan { get; set; }
        public string followUp { get; set; }
        public bool isDeleted { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime? modifiedDate { get; set; }
        public long createdBy { get; set; }
        public long? modifiedBy { get; set; }
        public List<TemplatePrescriptionModel> templatePrescriptionModel { get; set; }

    }

    public class TemplatePrescriptionModel
    {
        public TemplatePrescriptionModel()
        {

        }

        public long id { get; set; }
        public long templateMasterId { get; set; }
        public string medicineName { get; set; }
        public string strength { get; set; }
        public bool beforeFood { get; set; }
        public bool morning { get; set; }
        public bool noon { get; set; }
        public bool night { get; set; }
        public string remarks { get; set; }
        public int noOfDays { get; set; }
        public bool isDeleted { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime? modifiedDate { get; set; }
        public long createdBy { get; set; }
        public long? modifiedBy { get; set; }
    }

    public class TemplateQueryModel
    {
        public string name { get; set; }
        public string orderBy { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
    }
}
