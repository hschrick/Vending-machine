using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
  public class Money
    {
        public int _numberOfCoins { get; set; }
        public int _value { get; set; }
        public int _numCoinReturn { get; set; }
        public CoinDispenser _coinDispenser { get; set; }
        

        public Money(int numCoins, int value, CoinDispenser coinDispenser)
        {
            this._numberOfCoins = numCoins;
            this._value = value;
            this._coinDispenser = coinDispenser;
        }

        public void Inserted()
        {
            _numberOfCoins++;
        }

        public void ChangeReturn()
        {
            _numCoinReturn++;
            _numberOfCoins--;
        }

        public void DisplayReturn()
        {
            _coinDispenser.Actuate(_numCoinReturn);
        }
    }
}
