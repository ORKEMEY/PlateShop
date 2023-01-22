import Good from './good.model';

export default class Order {
  constructor(
    public address: String,
    public userId: Number,
    public goods?: Good[],
    public id?: Number,
  ) {}
}
