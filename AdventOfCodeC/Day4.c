#include <stdio.h>
#include <stdlib.h>

#define BUFFER_SIZE 1000

void solve(FILE *input, int part2){
    char line[BUFFER_SIZE];
    int pairs = 0;

    while(fgets(line, sizeof(line), input)){
        int firstMin, firstMax, secondMin, secondMax;
        sscanf(line, "%d-%d,%d-%d", &firstMin, &firstMax, &secondMin, &secondMax);

        if(firstMin <= secondMin && firstMax >= secondMax)
            pairs++;
        else if(secondMin <= firstMin && secondMax >= firstMax)
            pairs++;
        else if((firstMax < secondMin || firstMin > secondMax) && part2)
            pairs++;
    }

    printf("Pairs: %d\n", pairs);
}

int main(){
    FILE *input = fopen("../input/2022/day4.txt", "r");

    solve(input, 0);
    rewind(input);
    solve(input, 1);
    fclose(input);
    
    return 0;
}