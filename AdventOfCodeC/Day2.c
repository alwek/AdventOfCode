#include <stdio.h>

#define BUFFER_SIZE 100
#define OPPONENT_OFFSET 64
#define PLAYER_OFFSET 87
#define ROCK 1
#define PAPER 2
#define SCISSOR 3

struct Moves {
    char opponent;
    char player;
};

void solve1(FILE *input){
    char line[BUFFER_SIZE];
    struct Moves turn;
    int score = 0;

    while(fgets(line, sizeof(line), input)) {
        turn.opponent = line[0];
        turn.player = line[2];

        // Add score for the move made
        score += turn.player - PLAYER_OFFSET;

        // Add score for the result, 3 points ig draw or 6 points for win
        if(turn.opponent - OPPONENT_OFFSET == turn.player - PLAYER_OFFSET)
            score += 3;
        else if(turn.opponent - OPPONENT_OFFSET == ROCK && turn.player - PLAYER_OFFSET == PAPER)
            score += 6;
        else if(turn.opponent - OPPONENT_OFFSET == PAPER && turn.player - PLAYER_OFFSET == SCISSOR)
            score += 6;
        else if(turn.opponent - OPPONENT_OFFSET == SCISSOR && turn.player - PLAYER_OFFSET == ROCK)
            score += 6;
    }

    printf("Score: %d\n", score);
}

void solve2(FILE *input){
    char line[BUFFER_SIZE];
    struct Moves turn;
    int score = 0;

    while(fgets(line, sizeof(line), input)) {
        turn.opponent = line[0];
        turn.player = line[2];

        if(turn.player - PLAYER_OFFSET == ROCK){
            // We need to lose this turn
            
            if(turn.opponent - OPPONENT_OFFSET == ROCK)
                score += SCISSOR;
            else if(turn.opponent - OPPONENT_OFFSET == PAPER)
                score += ROCK;
            else
                score += PAPER;
        }
        else if(turn.player - PLAYER_OFFSET == PAPER){
            // We need to draw this turn
            score += 3;

            if(turn.opponent - OPPONENT_OFFSET == ROCK)
                score += ROCK;
            else if(turn.opponent - OPPONENT_OFFSET == PAPER)
                score += PAPER;
            else
                score += SCISSOR;
        }
        else{
            // We need to draw this turn
            score += 6;

            if(turn.opponent - OPPONENT_OFFSET == ROCK)
                score += PAPER;
            else if(turn.opponent - OPPONENT_OFFSET == PAPER)
                score += SCISSOR;
            else
                score += ROCK;
        }
    }

    printf("Score: %d\n", score);
}

int main(){
    FILE *input = fopen("../input/2022/day2.txt", "r");

    solve1(input);
    rewind(input);
    solve2(input);
    fclose(input);
    
    return 0;
}