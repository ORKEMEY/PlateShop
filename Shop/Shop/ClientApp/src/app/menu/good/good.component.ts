import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observer } from 'rxjs';
// import { FormGroup, FormArray } from '@angular/forms';
import Good from '../../core/models/good.model';
import GoodService from '../../core/services/good.service';
import BasketService from '../../core/services/basket.service';

@Component({
  selector: 'good',
  templateUrl: 'good.component.html',
  styleUrls: ['good.component.css'],
})
export default class GoodComponent {
  public id: Number;

  public good: Good = null;

  constructor(
    private activatedRoute: ActivatedRoute,
    private goodService: GoodService,
    private basketService: BasketService,
  ) {
    const id = Number.parseInt(this.activatedRoute.snapshot.params.id, 10);
    this.goodService.getById(id, {
      next: (data) => {
        this.good = data as Good;
      },
    } as Observer<Good>);
  }

  public onAdd() {
    this.basketService.addGood(this.good);
    console.log(this.basketService.goods);
  }

  /*
  constructor(
    private activatedRoute: ActivatedRoute,
    private testService: TestService,
    // private questionService: QuestionService,
    private userService: UserServise,
    private testCheckService: TestCheckService,
  ) {
    super();

    const id = Number.parseInt(this.activatedRoute.snapshot.params.id, 10);

    this.testService.getById(id, {
      next: (data) => {
        this.test = data;
        this.questions = this.test?.questions;
      },
    } as Observer<Test>);

    /* this.testService.dataTests$.subscribe({
      // development only
      next: (data) => {
        this.test = data?.find?.((el) => el.id === id);
      },
    } as Observer<Test[]>);
    this.testService.refreshTests();

    this.questionService.dataQuestions$.subscribe((data) => {
      // development only
      this.questions = data;
    });

    this.questionService.refreshQuestions(); // development only 
  } 
  */
}
