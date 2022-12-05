import { off } from "process";
import { Task } from "../task";

export class Day02 extends Task {
  partOne(): String {
    let turns = this.readInput().split("\n");

    let totalPoints = 0;
    for (let i = 0; i < turns.length; i++) {
      let turn = turns[i].split(" ");

      let elf = turn[0];
      let me = turn[1];

      totalPoints += this.getPointsPartOne(elf, me);
    }

    return `${totalPoints}`;
  }
  partTwo(): String {
    let turns = this.readInput().split("\n");

    let totalPoints = 0;
    for (let i = 0; i < turns.length; i++) {
      let turn = turns[i].split(" ");

      let elf = turn[0];
      let me = turn[1];

      totalPoints += this.getPointsPartTwo(elf, me);
    }
    return `${totalPoints}`;
  }
  getPointsPartOne(elf: string, me: string): number {
    let points = 0;
    if (me === "X") {
      points += 1;
      if (elf === "C") {
        points += 6;
      } else if (elf === "A") {
        points += 3;
      }
    } else if (me === "Y") {
      points += 2;
      if (elf === "A") {
        points += 6;
      } else if (elf === "B") {
        points += 3;
      }
    } else if (me === "Z") {
      points += 3;
      if (elf === "B") {
        points += 6;
      } else if (elf === "C") {
        points += 3;
      }
    }
    return points;
  }
  getPointsPartTwo(elf: string, outcome: string): number {
    let points = 0;
    //A for Rock, B for Paper, and C for Scissors
    //X for Rock, Y for Paper, and Z for Scissors
    //Rock defeats Scissors, Scissors defeats Paper, and Paper defeats Rock
    //1 for Rock, 2 for Paper, and 3 for Scissors

    if (outcome === "X") {
      // Needs to loose
      if (elf === "A") {
        // Need to play scissor, Z
        points += 3;
      } else if (elf === "B") {
        // Need to play rock, X
        points += 1;
      } else if (elf === "C") {
        // Need to play paper, Y
        points += 2;
      }
    } else if (outcome === "Y") {
      // Needs a draw
      points += 3;
      if (elf === "A") {
        // Need to play rock, X
        points += 1;
      } else if (elf === "B") {
        // Need to play paper, Y
        points += 2;
      } else if (elf === "C") {
        // Need to play scissor, Z
        points += 3;
      }
    } else if (outcome === "Z") {
      // Needs to win
      points += 6;
      if (elf === "A") {
        // Need to play paper, Y
        points += 2;
      } else if (elf === "B") {
        // Need to play scissor, Z
        points += 3;
      } else if (elf === "C") {
        // Need to play rock, X
        points += 1;
      }
    }
    return points;
  }
}
