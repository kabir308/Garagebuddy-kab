const CACHE_NAME = 'garage-buddy-cache-v1';
const urlsToCache = [
    '/',
    '/css/site.min.css',
    '/js/site.min.js',
    '/themes/mazer/dist/assets/compiled/css/app.css',
    '/themes/mazer/dist/assets/compiled/js/app.js'
];

self.addEventListener('install', event => {
    event.waitUntil(
        caches.open(CACHE_NAME)
            .then(cache => {
                console.log('Opened cache');
                return cache.addAll(urlsToCache);
            })
    );
});

self.addEventListener('fetch', event => {
    event.respondWith(
        caches.open(CACHE_NAME).then(cache => {
            return fetch(event.request)
                .then(response => {
                    if (response.status === 200) {
                        cache.put(event.request, response.clone());
                    }
                    return response;
                })
                .catch(() => {
                    return cache.match(event.request);
                });
        })
    );
});
