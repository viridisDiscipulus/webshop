import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Observable } from "rxjs";
import { LoadService } from "../services/load.service";
import { delay, finalize } from "rxjs/operators";
import { Injectable } from "@angular/core";

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
    constructor(private loadService: LoadService) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (!req.url.includes('emailProvjera')){
            this.loadService.load();
        }
        return next.handle(req).pipe(
            delay(1000),
            finalize(() => {
                this.loadService.blank();
            })
        );
    }
}