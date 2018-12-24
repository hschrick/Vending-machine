using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
   public class Can
    {
        public string _name { get; set; }
        public int _stock { get; set; }
        public int _price { get; set; }
        public CanDispenser _canDispenser { get; set; }
        public Light _purchaseLight { get; set; }
        public Light _soldoutLight { get; set; }

        public Can(string name, int stock, int price, CanDispenser canDispenser, Light purchaseLight, Light soldOutLight)
        {
            this._name = name;
            this._stock = stock;
            this._price = price;
            this._canDispenser = canDispenser;
            this._purchaseLight = purchaseLight;
            this._soldoutLight = soldOutLight;
        }

        public void Dispense()
        {
            _canDispenser.Actuate();
        }

        public void TurnOnPurchaseLight()
        {
            _purchaseLight.TurnOn();
        }

        public void TurnOffPurchaseLight()
        {
            _purchaseLight.TurnOff();
        }

        public void turnOnSoldoutLight()
        {
            _soldoutLight.TurnOn();
        }
    }
}
