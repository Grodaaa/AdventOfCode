import { Task } from "../task";
import { fchownSync, readFileSync } from "fs";
import { join } from "path";
import { match } from "assert";

export class Day03 extends Task {
  partOne(): String {
    let rucksacks = this.readInput().split("\n");
    let prioList = this.getPrioList();

    let sum = 0;
    rucksacks.forEach((rucksack) => {
      let partSum = 0;
      let letters = rucksack.split("");
      let compartmentOne = letters.slice(0, letters.length / 2);
      let compartmentTwo = letters.slice(letters.length / 2, letters.length);

      let matches = compartmentTwo.filter(function (obj) {
        return compartmentOne.indexOf(obj) !== -1;
      });
      matches = matches.filter((n, i) => matches.indexOf(n) === i);

      matches.forEach((match) => {
        partSum += prioList.get(match) ?? 0;
      });

      sum += partSum;
    });

    return `${sum}`;
  }
  partTwo(): String {
    let rucksacks = this.readInput().split("\n");
    let prioList = this.getPrioList();

    let sum = 0;
    for (let i = 0; i < rucksacks.length; i = i + 3) {
      let partSum = 0;
      let ruggsackOne = rucksacks[i].split("");
      let rugsackTwo = rucksacks[i + 1].split("");
      let rugsackThree = rucksacks[i + 2].split("");

      let matches = ruggsackOne.filter(function (obj) {
        return (
          rugsackTwo.indexOf(obj) !== -1 && rugsackThree.indexOf(obj) !== -1
        );
      });
      matches = matches.filter((n, i) => matches.indexOf(n) === i);

      matches.forEach((match) => {
        partSum += prioList.get(match) ?? 0;
      });

      sum += partSum;
    }

    return `${sum}`;
  }
  getPrioList(): Map<string, number> {
    let map: Map<string, number> = new Map();
    const path = "./inputs/prio_list_input.txt";
    let input = readFileSync(path, "utf-8").split("\n");

    input.forEach((element) => {
      let prio = element.split(" ");
      map.set(prio[0], parseInt(prio[1]));
    });

    return map;
  }
}
