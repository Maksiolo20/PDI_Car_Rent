import http from 'k6/http';
import { sleep, check } from 'k6';

export const options = {
    stages: [
        { duration: '10s', target: 200 },
        { duration: '30s', target: 200 },
        { duration: '10s', target: 0 },
    ],
    thresholds: {
        http_req_duration: ['p(99)<500'],
    },
};

export default () => {
    const res = http.get('http://localhost:5022/cars');
    check(res, { 'status is 200': (r) => r.status === 200 });
    sleep(1);
};
