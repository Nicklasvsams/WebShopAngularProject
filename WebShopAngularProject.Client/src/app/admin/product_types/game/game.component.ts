import { Component, OnInit } from '@angular/core';
import { Game } from 'src/app/_models/product_types/game';
import { GameService } from 'src/app/_services/product_types/game.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {

  game: Game = { id: 0, publisher: '', publishedYear: 0, language: '', genre: '', productId: 0, categoryId: 0 }
  games: Game[] = [];

  constructor(private gameService: GameService) { }

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
  }

  save(): void {
    if (this.game.id == 0) {
      if (confirm('Save new game?')) {
        this.gameService.createGame(this.game)
          .subscribe({
            next: (x) => {
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
