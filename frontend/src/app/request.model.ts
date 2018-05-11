export class RequestModel {
  primeText: string;
  numberOfLines: number;

  constructor(primeText: string, numberOfLines: number) {
    this.primeText = primeText;
    this.numberOfLines = numberOfLines;
  }
}
