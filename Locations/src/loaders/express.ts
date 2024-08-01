import express from 'express';
import cors from 'cors';

import routes from '../api/index.js';

export default async ({app}: { app: express.Application }) => {
    app.get('/health', (req, res) => {
        res.send("healthy").status(200).end();
    });
    app.head('/health', (req, res) => {
        res.send("healthy").status(200).end();
    });


    app.use(cors());
    app.use(express.json());
    app.use("/", routes());

    app.use((req, res, next) => {
        const err = new Error('Not Found');
        err['status'] = 404;
        next(err);
    });

    app.use((err, req, res, next) => {
        res.status(err.status || 500);
        res.json({
            errors: {
                message: err.message,
            },
        });
    });
};
