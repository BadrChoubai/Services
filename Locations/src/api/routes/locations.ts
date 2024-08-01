import { Router, Request, Response } from 'express';
const router = Router();

export default (app: Router) => {
    app.use('/locations', router);

    router.get("/", (req: Request, res: Response) => {
        return res.json({
            count: 0,
            locations: [],
        }).status(200);
    });
};