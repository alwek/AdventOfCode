#include <iostream>
#include <fstream>
#include <vector>
#include <string>
#include <algorithm>

class Day1 {
    public: static void solve(std::string path) {
        std::cout << "Day One" << std::endl;
        std::vector<std::string> input = readFile(path);

        std::vector<int> calories;
        int calorie = 0;

        for (int i = 0; i < input.size(); i++) {
            try {
                calorie += std::stoi(input[i]);
            }
            catch (std::invalid_argument) {
                calories.push_back(calorie);
                calorie = 0;
            }
        }

        std::sort(calories.begin(), calories.end(), std::greater<int>());

        std::cout << "Max calorie: " << calories[0] << std::endl;
        std::cout << "Top three: " << calories[0] + calories[1] + calories[2] << std::endl;
    }

    private: static std::vector<std::string> readFile(std::string path) {
        std::ifstream file(path);

        if (!file.is_open()) {
            std::cerr << "Failed to open the file." << std::endl;
            exit(1);
        }

        std::vector<std::string> input;
        std::string line;

        while (std::getline(file, line)) {
            input.push_back(line);
        }

        file.close();

        return input;
    }
};