// SPDX-License-Identifier: MIT
pragma solidity >=0.4.0 <=0.8.17;

contract Payment {

    address public seller;

    constructor() payable {
        seller = msg.sender;
    }

    function purchase() public payable
    {
        require(msg.sender.balance > 0,"Not enough money.");
    }

    function getBalance() public payable returns (uint balance)
    {
        balance = address(this).balance; 
        return balance;
    }

}