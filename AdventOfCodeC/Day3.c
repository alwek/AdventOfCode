#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <ctype.h>

#define BUFFER_SIZE 1000
#define LOWER_CHAR_OFFSET 96
#define UPPER_CHAR_OFFSET 38

void solve(FILE *input){
    char line[BUFFER_SIZE];
    char letters[BUFFER_SIZE];
    int index = 0;
    int sum = 0;

    while(fgets(line, sizeof(line), input)){
        int length = strlen(line) - 1;
        int midpoint = length / 2;

        char *first = (char *) malloc(midpoint * sizeof(char));
        char *second = (char *) malloc(midpoint * sizeof(char));

        memcpy(first, line, midpoint);
        memcpy(second, line + midpoint, midpoint);

        int found = 0;
        for(int i = 0; i < strlen(first); i++){
            for(int j = 0; j < strlen(second); j++){
                if(first[i] == second[j]){
                    letters[index] = first[i];
                    found = 1;
                    break;
                }
            }

            if(found){
                break;
            }
        }

        free(first);
        free(second);

        index++;
    }

    letters[index] = '\0';

    for(int i = 0; i < strlen(letters); i++){
        if(isupper(letters[i]))
            sum += letters[i] - UPPER_CHAR_OFFSET;
        else
            sum += letters[i] - LOWER_CHAR_OFFSET;
    }
    
    printf("Sum: %d\n", sum);
}

int main(){
    FILE *input = fopen("../input/2022/test.txt", "r");

    solve(input);
    
    return 0;
}