namespace FileIO {
    public class Duck {
        public int Duck_ID { get; set; }
        public string Duck_Color { get; set; }
        public int Duck_FeatherNum { get; set; } 

        public Duck() {

        }

        public Duck (int pID, string pColor, int pFeathers) {
            Duck_ID = pID;
            Duck_Color = pColor;
            Duck_FeatherNum = pFeathers;
        }

        public void Quack() {
            Console.WriteLine($"Quack! I'm Duck {Duck_ID} and I'm {Duck_Color}!");
        }

        public override string ToString() {
            return $"{Duck_ID} {Duck_Color} {Duck_FeatherNum}";
        }
    }
}