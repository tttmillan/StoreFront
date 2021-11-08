using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StoreFront.DATA.EF/*.Metadata*/
{
    #region Brand

    public class BrandMetadata
    {
        [Required(ErrorMessage = "**Brand Name is required**")]
        [StringLength(50, ErrorMessage = "**Brand Name cannot be more than 50 Characters**")]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }
    }

    [MetadataType(typeof(BrandMetadata))]
    public partial class Brand
    {

    }

    #endregion

    #region Color

    public class ColorMetadata
    {
        [Required(ErrorMessage = "**Color Name is required**")]
        [StringLength(50, ErrorMessage = "**Color Name cannot be more than 50 Characters**")]
        [Display(Name = "Color Name")]
        public string ColorName { get; set; }
    }

    [MetadataType(typeof(ColorMetadata))]
    public partial class Color
    {

    }

    #endregion

    #region Employee

    public class EmployeeMetadata
    {
        [Required(ErrorMessage = "**First Name is required**")]
        [StringLength(50, ErrorMessage = "**First Name cannot be more than 50 Characters**")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "**Last Name is required**")]
        [StringLength(50, ErrorMessage = "**Last Name cannot be more than 50 Characters**")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "**Employee Address is required**")]
        [StringLength(50, ErrorMessage = "**Employee Address cannot be more than 50 Characters**")]
        [Display(Name = "Employee Address")]
        public string EmpAdderss { get; set; }
        [StringLength(50, ErrorMessage = "**City cannot be more than 50 Characters**")]
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        public string City { get; set; }
        [StringLength(2, ErrorMessage = "**State must be 2 Characters**")]
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        public string State { get; set; }
        [Required(ErrorMessage = "**ReportID is required**")]
        [StringLength(50, ErrorMessage = "**ReportID cannot be more than 50 Characters**")]
        public string ReportID { get; set; }
        [Required(ErrorMessage = "**Department is required**")]
        [StringLength(50, ErrorMessage = "**Department cannot be more than 50 Characters**")]
        public string Department { get; set; }
    }

    [MetadataType(typeof(EmployeeMetadata))]
    public partial class Employee
    {
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

    }

    #endregion

    #region Personalized

    public class PersonalizedMetadata
    {
        [Required(ErrorMessage = "**Personalized Type is required**")]
        [StringLength(50, ErrorMessage = "**Personalized Type cannot be more than 50 Characters**")]
        [Display(Name = "Personalized Type")]
        public string PersonalizedType { get; set; }
    }

    [MetadataType(typeof(PersonalizedMetadata))]
    public partial class Personalized
    {

    }

    #endregion

    #region Product

    public class ProductMetadata
    {
        [Required(ErrorMessage = "**Product Name is required**")]
        [StringLength(50, ErrorMessage = "**Product Name cannot be more than 50 Characters**")]
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        //public int BrandID { get; set; }
        //public int TypeID { get; set; }
        //public int PersonalizedID { get; set; }
        [Display(Name = "Shipped Feature")]
        public bool IsShipped { get; set; }
        //public int EmployeeID { get; set; }
        [Display(Name = "BackOrder Feature")]
        public bool IsOnBackOrder { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "**Value must be a valid number, 0 or larger.")]
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        [Display(Name = "Units Sold")]
        public decimal UnitsSold { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "**Value must be a valid number, 0 or larger.")]
        [DisplayFormat(DataFormatString = "{0:c}", NullDisplayText = "[-N/A-")]
        public decimal Price { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "**Value must be a valid number, 0 or larger.")]
        [DisplayFormat(NullDisplayText = "[-N/A-")]
        public int Quantity { get; set; }
        //public int ColorID { get; set; }
    }

    [MetadataType(typeof(ProductMetadata))]
    public partial class Product
    {

    }

    #endregion

    #region ProductType

    public class ProductTypeMetadata
    {
        [Required(ErrorMessage = "**Product Type Name is required**")]
        [StringLength(50, ErrorMessage = "**Product Type Name cannot be more than 50 Characters**")]
        [Display(Name = "Product Type Name")]
        public string TypeName { get; set; }
    }

        [MetadataType(typeof(ProductTypeMetadata))]
    public partial class ProductType
    {

    }

    #endregion
}
