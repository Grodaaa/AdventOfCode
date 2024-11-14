import { Task } from "../task";

export class Day01 extends Task {
    partOne(): String {
        let elfs = this.getElfs();

        return `${elfs[0]}`;
    }
    partTwo(): String {
        let elfs = this.getElfs();

        const result = elfs[0] + elfs[1] + elfs[2];

        return `${result}`;
    }
    getElfs(): number[] {
        const input = this.readInput();
        let calorieList = input.split('\n');

        let elfs : number[] = [];
        
        let sumCalories = 0;
        calorieList.forEach(element => {
            if(Number.isNaN(parseInt(element))) {
                elfs.push(sumCalories);
                sumCalories = 0;
            } else {
                sumCalories += parseInt(element);
            }
        });

        elfs.sort((n1,n2) => n2 - n1);
        return elfs;
    }
}