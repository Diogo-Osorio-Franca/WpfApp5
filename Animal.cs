using System;

namespace WpfApp5

{
    public class Animal : ICloneable
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string raça { get; set; }
        public int idade { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }


}
