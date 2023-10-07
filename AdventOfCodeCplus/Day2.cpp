#include <iostream>
#include <fstream>
#include <vector>
#include <string>

class Day2 {
public: static void solve(std::string path) {
    std::cout << "Day Two" << std::endl;
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