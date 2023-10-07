#include <iostream>
#include <sstream>
#include <string>
#include "Day2.cpp"
#include "Day1.cpp"

std::string getInputPath(int day) {
    std::ostringstream oss;
    oss << "C:/Users/alica/source/repos/AdventOfCode/input/2022/day" << day << ".txt";

    return oss.str();
}

int main()
{
    std::cout << "Advent of Code 2022 - Alican Bircan" << std::endl;
    std::cout << "Choose day:" << std::endl;

    int day = 0;
    while (std::cin >> day) {
        switch (day) {
            case 1:
                Day1::solve(getInputPath(day));
                break;
            case 2:
                Day2::solve(getInputPath(day));
                break;
            default:
                std::cout << "Invalid day chosen, somehow.." << std::endl;
                break;
        }
    }
}