using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class Economy
    {
        private int _500Yen = 0;
        private int _100Yen = 0;
        private int _50Yen = 0;
        private int _10Yen = 0;
       private List<Can> _cans;
       private List<Money> _coins;
        public int _currentlyInserted { get; set; }
        private int _initialInserted;
        private TimerLight _noChangeLight { get; set; }



        public Economy()
        {

        }

        public void SetLists(List<Can> cans, List<Money> coins)
        {
            _cans = cans;
            _coins = coins;
        }

        public void SetNoChangeLight(TimerLight noChangeLight)
        {
            _noChangeLight = noChangeLight;
        }

        public void CoinInsert(int index)
        {
            _coins[index].Inserted();
            _currentlyInserted += _coins[index]._value;
            if (_coins[index]._value == 500) _500Yen++;
            if (_coins[index]._value == 100) _100Yen++;
            if (_coins[index]._value == 50)  _50Yen++;
            if (_coins[index]._value == 10)  _10Yen++;
        }

        public void TurnOnPurchaseLight()
        {
        _cans.ForEach(can => {
            if (_currentlyInserted >= can._price) can.TurnOnPurchaseLight();});
        }

        public void TurnOffPurchaseLight()
        {
            _cans.ForEach(can => can.TurnOffPurchaseLight());
        }
        
        public void PurchaseCan(int Index)
        {
            _initialInserted = _currentlyInserted;
            if (CheckCoins(_cans[Index]))
            {                                                                                             
                if (_cans[Index]._purchaseLight.IsOn() && _cans[Index]._stock > 0)
                {
                    _cans[Index]._stock--;
                    _currentlyInserted = _currentlyInserted - _cans[Index]._price;
                    AttemptReturnChange(_cans[Index]);
                    
                    if (_currentlyInserted != 0) _currentlyInserted = _initialInserted;
                    else
                    {
                        _cans.ForEach(can => {
                            if (can._stock == 0) can.turnOnSoldoutLight();
                            if (can._purchaseLight.IsOn()) can.TurnOffPurchaseLight();
                             });
                        _cans[Index].Dispense();
                    }
                }
                else return;
            }
            else
            {
                _currentlyInserted = _initialInserted;
                _noChangeLight.TurnOn3Sec();
            }
        }

        public bool CheckCoins(Can can)
          { 
            int coinIndex = 0;

            foreach (Money coin in _coins)
            {
                if (coin._numberOfCoins <= 0 || coin._value > _currentlyInserted) coinIndex++;
                if (coinIndex == 3 && _currentlyInserted > 0) return false;
            }
            return true;
        }

        public void AttemptReturnChange(Can canN)
        {
            int ticker = 0;
            int[] CoinIndex = new int[4];

            if (_currentlyInserted > 0)
            {
                foreach (Money coin in _coins)
                {
                    while (_currentlyInserted >= 0)
                    {
                        if (_currentlyInserted >= coin._value)
                        {
                            _currentlyInserted -= coin._value;
                            if (coin._numberOfCoins <= 0)
                            {
                                _currentlyInserted += coin._value;
                                break;
                            }
                            coin.ChangeReturn();
                            CoinIndex[ticker] = coin._numCoinReturn;
                            coin.DisplayReturn();
                        }
                        else break;
                    }
                    ticker++;
                    coin._numCoinReturn = 0;
                }
                //if Attempt has failed, machine refunds customer here.
                if (_currentlyInserted != 0) Refund(canN, CoinIndex);
            }
            else
                 {
                    foreach (Can can in _cans)
                    {
                       if (can._purchaseLight.IsOn()) can.TurnOffPurchaseLight();
                    }
                    return;
                 }
            }

        public void Refund(Can canN, int[] CoinIndex)
        {
            _coins[0]._numberOfCoins += CoinIndex[0];
            _coins[1]._numberOfCoins += CoinIndex[1];
            _coins[2]._numberOfCoins += CoinIndex[2];
            _coins[3]._numberOfCoins += CoinIndex[3];

            _coins[0]._numCoinReturn = 0;
            _coins[1]._numCoinReturn = 0;
            _coins[2]._numCoinReturn = 0;
            _coins[3]._numCoinReturn = 0;

            _coins[0].DisplayReturn();
            _coins[1].DisplayReturn();
            _coins[2].DisplayReturn();
            _coins[3].DisplayReturn();

            if (_currentlyInserted != 0)
            {
                _noChangeLight.TurnOn3Sec();
                canN._stock++;
                _coins.ForEach(coin =>
                {
                    while (coin._numCoinReturn != 0)
                    {
                        coin._numberOfCoins++;
                        coin._numCoinReturn--;
                    }
                });
            }
        }

        public void Reset()
        {
            _cans.ForEach(can =>
            {
                can._stock = 4;
                can._purchaseLight.TurnOff();
                can._canDispenser.Clear();
            });
            _coins.ForEach(coin =>
            {
                coin._coinDispenser.Clear();
                coin._numCoinReturn = 0;
            });

            _coins[3]._numberOfCoins = 15;
            _coins[2]._numberOfCoins = 10;
            _coins[1]._numberOfCoins = 5;
            _coins[0]._numberOfCoins = 2;
            _currentlyInserted = 0;

            _cans[3]._soldoutLight.TurnOff();
            _cans[2]._soldoutLight.TurnOff();
            _cans[1]._soldoutLight.TurnOff();
            _cans[0]._soldoutLight.TurnOff();
        }
    }
}
