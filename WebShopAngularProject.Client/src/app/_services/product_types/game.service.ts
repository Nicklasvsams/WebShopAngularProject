import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Game } from 'src/app/_models/product_types/game';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  private apiUrl = environment.apiUrl + 'game/';
  private httpOptions = {

    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  constructor(private http: HttpClient) { }

  getAllGames(): Observable<Game[]> {
    return this.http.get<Game[]>(this.apiUrl);
  }

  getGame(gameId: number): Observable<Game> {
    return this.http.get<Game>(this.apiUrl + gameId);
  }

  deleteGame(gameId: number): Observable<Game> {
    return this.http.delete<Game>(this.apiUrl + gameId, this.httpOptions);
  }

  createGame(game: Game): Observable<Game> {
    return this.http.post<Game>(this.apiUrl, game, this.httpOptions);
  }

  updateGame(gameId: number, game: Game): Observable<Game> {
    return this.http.put<Game>(this.apiUrl + gameId, game, this.httpOptions);
  }
}
