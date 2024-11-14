import { Day01 } from "./day01/day01";
import { Day02 } from "./day02/day02";
import { Day03 } from "./day03/day03";
import { Day04 } from "./day04/day04";
import { Task } from "./task";

console.log("Advent of Code 2022 🎄");

type NullableTask = Task | null;

if (process.argv.length < 3) {
  console.error("Invalid amount of arguments");
  process.exit(1);
}

const date = parseInt(process.argv[2]);

if (date == null || Number.isNaN(date) || date < 1 || date > 25) {
  console.error(`${date} is invalid!`);
  process.exit(1);
}

let day: NullableTask = null;

switch (date) {
  case 1:
    day = new Day01();
    break;
  case 2:
    day = new Day02();
    break;
  case 3:
    day = new Day03();
    break;
  case 4:
    day = new Day04();
    break;
}

if (day == null) {
  console.error("Day not found");
  process.exit(1);
}

new Map([
  [
    "One",
    () => {
      return day!.partOne();
    },
  ],
  [
    "Two",
    () => {
      return day!.partTwo();
    },
  ],
]).forEach((value, key) => {
  const startTime = Date.now();
  const result = value();
  const endTime = Date.now();

  console.log(
    `Part ${key}: ${result} (took: ${formatMs(
      endTime.valueOf() - startTime.valueOf()
    )})`
  );
});

function formatMs(ms: number): string {
  const minutes = Math.floor(ms / 60000);
  const seconds = ms / 1000.0;

  const secondsStr =
    seconds < 10
      ? "0" + String(seconds.toPrecision(7))
      : String(seconds.toPrecision(7));
  const minutesStr = minutes < 10 ? "0" + String(minutes) : String(minutes);

  return `${minutesStr}:${secondsStr}`;
}
