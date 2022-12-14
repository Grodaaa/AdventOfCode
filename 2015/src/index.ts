import { Day01 } from "./day01/day01";
import { Day02 } from "./day02/day02";
import { Task } from "./task";

console.log("Advent of Code 2015 🎄");

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
