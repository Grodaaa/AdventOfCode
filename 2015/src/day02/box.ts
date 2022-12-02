export class Box {
  length: number = 0;
  width: number = 0;
  height: number = 0;
  surface: number = 0;
  slack: number = 0;

  paperNeeded: number = 0;

  constructor(length: number, width: number, height: number) {
    this.length = length;
    this.width = width;
    this.height = height;

    this.surface =
      2 * this.length * this.width +
      2 * this.width * this.height +
      2 * this.height * this.length;

    this.slack = Math.min(
      ...[
        this.length * this.width,
        this.width * this.height,
        this.height * this.length,
      ]
    );

    this.paperNeeded = this.surface + this.slack;
  }
}
