import { Task } from "../task";
import { Box } from "./box";
import { Ribbon } from "./ribbon";

export class Day02 extends Task {
  partOne(): String {
    let boxes = this.getBoxes();

    let paperNeeded = boxes.reduce((arr, val) => {
      return arr + val.paperNeeded;
    }, 0);

    return `${paperNeeded}`;
  }
  partTwo(): String {
    let ribbons = this.getRibbons();

    let ribbonNeeded = ribbons.reduce((arr, val) => {
      return arr + val.totalLength;
    }, 0);

    console.log(ribbons[0]);
    return `${ribbonNeeded}`;
  }
  getBoxes(): Box[] {
    let boxesInput = this.readInput().split("\n");
    let boxes: Box[] = [];

    boxesInput.forEach((element) => {
      let dimensions = element.split("x");
      boxes.push(
        new Box(
          parseInt(dimensions[0]),
          parseInt(dimensions[1]),
          parseInt(dimensions[2])
        )
      );
    });

    return boxes;
  }
  getRibbons(): Ribbon[] {
    let ribbonsInput = this.readInput().split("\n");
    let ribbons: Ribbon[] = [];

    ribbonsInput.forEach((element) => {
      let dimensions = element.split("x").map((str) => {
        return Number(str);
      });
      ribbons.push(new Ribbon(dimensions));
    });

    return ribbons;
  }
}
