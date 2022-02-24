import { Component, OnInit } from '@angular/core';
import { Category } from 'src/app/_models/category';
import { Product } from 'src/app/_models/product';
import { Game } from 'src/app/_models/product_types/game';
import { CategoryService } from 'src/app/_services/category.service';
import { ProductService } from 'src/app/_services/product.service';
import { GameService } from 'src/app/_services/product_types/game.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {

  game: Game = { id: 0, publisher: '', publishedYear: 0, language: '', genre: '', productId: 0, categoryId: 0 }
  games: Game[] = [];
  products: Product[] = [];
  categories: Category[] = [];

  constructor(private gameService: GameService, private productService: ProductService, private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.gameService.getAllGames()
      .subscribe({
        next: (x) => {
          this.games = x;
        },
        error: (err) => {
          console.log(err);
        }
      })

    this.productService.getAllProducts()
      .subscribe({
        next: (x) => {
          this.products = x;
        },
        error: (err) => {
          console.log(err);
        }
      })

    this.categoryService.getAllCategories()
      .subscribe({
        next: (x) => {
          this.categories = x;
        },
        error: (err) => {
          console.log(err);
        }
      })
  }

  save(): void {
    if (this.game.id == 0) {
      if (confirm('Save new game?')) {
        this.gameService.createGame(this.game)
          .subscribe({
            next: (x) => {
              x.category = this.categories.find(y => y.id == x.categoryId);
              x.product = this.products.find(y => y.id == x.productId);
              console.log(x);

              this.games.push(x);
              this.game = this.resetGame();
            },
            error: (err) => {
              console.log(err);
            }
          });
      }
    } else {
      if (confirm('Update game with ID ' + this.game.id + '?')) {
        this.gameService.updateGame(this.game.id, this.game)
          .subscribe({
            next: () => {
              this.game = this.resetGame();
            },
            error: (err) => {
              console.log(err);
            }
          });
      };
    };
  }

  edit(game: Game): void {
    if (confirm('Do you want to edit this game?')) {
      this.game = game;
    };
  };

  delete(game: Game): void {
    if (confirm('Do you want to delete this game?')) {
      this.gameService.deleteGame(game.id)
        .subscribe({
          next: (x) => {
            this.games = this.games.filter(x => x.id != game.id);
          },
          error: (err) => {
            console.log(err);
          }
        });
    };
  }

  cancel(): void {
    this.game = this.resetGame();
  };

  resetGame(): Game {
    var game: Game = { id: 0, publisher: '', publishedYear: 0, language: '', genre: '', productId: 0, categoryId: 0 }
    return game;
  };
}
