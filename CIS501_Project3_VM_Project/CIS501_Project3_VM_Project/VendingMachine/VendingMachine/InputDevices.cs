//////////////////////////////////////////////////////////////////////
//      Vending Machine (Actuators.cs)                              //
//      Written by Masaaki Mizuno, (c) 2006, 2007, 2008, 2010, 2011 //
//                      for Learning Tree Course 123P, 252J, 230Y   //
//                 also for KSU Course CIS501                       //  
//////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    // For each class, you can (must) add fields and overriding constructors

    public class CoinInserter
    {
        // add a field to specify an object that CoinInserted() will firstvisit
       private int _index;
       private Economy _economy;
        // rewrite the following constructor with a constructor that takes an object
        // to be set to the above field
        public CoinInserter(Economy economy, int index)
        {
            _index = index;
            _economy = economy;
        }
        public void CoinInserted()
        {
            // You can add only one line here
            _economy.CoinInsert(_index);
            _economy.TurnOnPurchaseLight();
        }
    }

    public class PurchaseButton
    {
        private int _index;
       private Economy _economy;
        // add a field to specify an object that ButtonPressed() will first visit
        public PurchaseButton(Economy economy,int index)
        {
            _economy = economy;
            _index = index;
        }
        public void ButtonPressed()
        {
            // You can add only one line here
            _economy.PurchaseCan(_index);
        }
    }

    public class CoinReturnButton
    {
       private Economy _economy;
       private Can _can;
        // add a field to specify an object that Button Pressed will visit
        // replace the following default constructor with a constructor that takes
        // an object to be set to the above field
        public CoinReturnButton(Economy economy)
        {
            _economy = economy;
        }
        public void ButtonPressed()
        {
            // You can add only one lines here
            _economy.AttemptReturnChange(_can);
            _economy.TurnOffPurchaseLight();
        }
    }
}
