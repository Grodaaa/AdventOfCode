import { Task } from "../task";

export class Day04 extends Task {
  partOne(): String {
    let sections = this.readInput().split("\n");

    let pairs = 0;

    sections.forEach((pair) => {
      let elfs = pair.split(",");
      let elfOne = elfs[0].split("-");
      let elfTwo = elfs[1].split("-");

      if (
        (parseInt(elfOne[0]) <= parseInt(elfTwo[0]) &&
          parseInt(elfOne[1]) >= parseInt(elfTwo[1])) ||
        (parseInt(elfTwo[0]) <= parseInt(elfOne[0]) &&
          parseInt(elfTwo[1]) >= parseInt(elfOne[1]))
      ) {
        pairs++;
      }
    });

    return `${pairs}`;
  }
  partTwo(): String {
    let elfPairSections = this.readInput().split("\n");

    let overlaps = 0;
    elfPairSections.forEach((elfPair) => {
      let elfs = elfPair.split(",");
      let elfOne = this.getInterval(elfs[0].split("-"));
      let elfTwo = this.getInterval(elfs[1].split("-"));

      let overlap = elfOne.filter(function (obj) {
        return elfTwo.indexOf(obj) !== -1;
      });

      if(overlap.length > 0) overlaps++;
    });

    return `${overlaps}`;
  }
  getInterval(sectionString: string[]): number[] {
    let interval: number[] = [];
    let start = parseInt(sectionString[0]);
    let end = parseInt(sectionString[sectionString.length - 1]);

    for (let i = start; i <= end; i++) {
      interval.push(i);
    }

    return interval;
  }
}
