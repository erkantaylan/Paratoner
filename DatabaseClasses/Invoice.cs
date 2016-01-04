namespace DatabaseClasses {

    public class Invoice {
        public int InvocieId { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public string BuyDate { get; set; }
        public double Price { get; set; }
        public bool IsExpired { get; set; }

        public override string ToString() {
            var all = $"invoiceId:{this.InvocieId}," +
                      $"userId:{this.UserId}," +
                      $"groupId:{this.GroupId}," +
                      $"buyDate:{this.BuyDate}," +
                      $"price:{this.Price}," +
                      $"isExpired:{this.IsExpired}";
            return all;
        }
    }

}