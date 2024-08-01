import config from './config/index.js';

import express from 'express';

const startServer = async () => {
    const app = express();

    await import('./loaders/index.js').then(pkg => {
        return pkg.default(app);
    });

    app.listen(config.port, () => {
        console.log(`http://127.0.0.1:${config.port}`);
    });
};

await startServer();