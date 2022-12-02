export class Ribbon {
  lengthOfRibbon: number = 0;
  lengthOfBow: number = 0;
  totalLength: number = 0;

  constructor(sides: number[]) {
    sides = sides.sort((n1, n2) => n1 - n2);
    this.lengthOfRibbon = sides[0] + sides[0] + sides[1] + sides[1];
    this.lengthOfBow = sides[0] * sides[1] * sides[2];

    this.totalLength = this.lengthOfRibbon + this.lengthOfBow;
  }
}
