import { readFileSync } from 'fs';
import { fileURLToPath } from 'url';
import { dirname, join } from 'path';

const __filename = fileURLToPath(import.meta.url);
const __dirname = dirname(__filename);

const configFilePath = join(__dirname, '../../config.json');
console.log('Config file path: ', configFilePath);

let config: any;

try {
    const rawData = readFileSync(configFilePath, 'utf-8');
    config = JSON.parse(rawData);
} catch (error) {
    console.error('error reading or parsing configuration file:', error);
}

// Set the NODE_ENV to 'development' by default
process.env.NODE_ENV = process.env.NODE_ENV || 'development';

export default {
    api: {
        prefix: '/api',
    },
    port: config.port,
};