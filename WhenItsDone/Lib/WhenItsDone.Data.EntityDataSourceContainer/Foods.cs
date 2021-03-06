//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WhenItsDone.Data.EntityDataSourceContainer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Foods
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Foods()
        {
            this.Ingredients = new HashSet<Ingredients>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int IngredientType { get; set; }
        public int MeasurementUnitType { get; set; }
        public decimal PricePerUnit { get; set; }
        public int NutritionFactsId { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual NutritionFacts NutritionFacts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ingredients> Ingredients { get; set; }
    }
}
