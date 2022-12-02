import { Task } from "../task";

export class Day01 extends Task {
  partOne(): String {
    let input = this.readInput();
    let instructions = input.split("");

    const map: Map<string, number> = new Map();

    instructions.forEach((element) => {
      let item = map.get(element);

      if (item === undefined) item = 0;

      map.set(element, element === "(" ? item + 1 : item - 1);
    });

    let floor = Array.from(map.values()).reduce((acc, val) => acc + val);
    return `${floor}`;
  }
  partTwo(): String {
    let input = this.readInput();
    let instructions = input.split("");

    let floor = 0;
    let position = 0;
    for (let element of instructions) {
      if (element === "(") {
        floor++;
      } else {
        floor--;
      }
      position++;

      if (floor === -1) break;
    }

    return `${position}`;
  }
}
