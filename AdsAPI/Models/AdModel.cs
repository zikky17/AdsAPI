namespace AdsAPI.Models
{
    public class AdModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Created { get; set; }

        public AdModel()
        {
            Created = DateTime.Now.ToShortDateString();
        }
    }
}
