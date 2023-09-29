#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define BUFFER_SIZE 100
#define TOP_COUNT 3

int main(){
    FILE *input = fopen("../input/2022/day1.txt", "r");

    if (input == NULL) {
        perror("Error opening the file");
        return 1;
    }

    char line[BUFFER_SIZE];
    int top[TOP_COUNT] = {0};
    int sum = 0, max = 0;

    while(fgets(line, sizeof(line), input)) {
        if(line[0] == '\n' || strlen(line) == 0){
            if(sum > max)
                max = sum;

            for (int i = 0; i < TOP_COUNT; i++) {
                if (sum > top[i]) {
                    for (int j = TOP_COUNT - 1; j > i; j--) 
                        top[j] = top[j - 1];
                    top[i] = sum;
                    break;
                }
            }

            sum = 0;
        }
        else {
            int value = 0;
            if (sscanf(line, "%d", &value) == 1)
                sum += value;
        }
    }

    printf("Max calories: %d\n", max);
    printf("Top calories: %d\n", top[0] + top[1] + top[2]);

    fclose(input);
    return 0;
}