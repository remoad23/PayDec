// SPDX-License-Identifier: MIT
pragma solidity >=0.4.0 <=0.8.17;

error NotEnoughMoney(uint money, uint requiredMoney);

contract Payment {

    address payable public seller;

    constructor() payable {
        seller = payable(msg.sender);
    }

    function purchase(address payable buyer,uint amount) public payable
    {
        uint gas = gasleft();

        // bezahlung
        // && buyer.balance >= amount
        if(amount > 0 && buyer.balance > 0)
        {
            uint gasToPay = gas - gasleft();
            buyer.transfer(amount);
        }
        // failed 
        else
        {
            revert NotEnoughMoney({
                money: buyer.balance,
                requiredMoney: amount
            });
        }
    }

    function getBalance() public payable returns (uint balance)
    {
        balance = address(this).balance; 
        return balance;
    }

}