// // In development, always fetch from the network and do not enable offline support.
// // This is because caching would make development more difficult (changes would not
// // be reflected on the first load after each change).
// // self.addEventListener('fetch', () => { });
//
//
// const CACHE_NAME = 'version-17';
// const urlsToCache = [ 'index.html' ];
// const extensionsToCache = [
//     'js',
//     'dll',
//     'css',
//     'png',
//     'woff2',
//     'tif',
//     'jpg',
//     'glb'
// ];
//
// const self = this;
//
// // Install SW
// self.addEventListener('install', (event) => {
//     console.log("--service install called" , CACHE_NAME);
//     // event.waitUntil(
//     //     caches.open(CACHE_NAME)
//     //         .then((cache) => {
//     //             console.log('Opened cache');
//     //
//     //             return cache.addAll(urlsToCache);
//     //         })
//     // )
// });
//
// // Listen for requests
// self.addEventListener('fetch', (event) => {
//    
//     const request = event.request;
//     const url = new URL(request.url);
//     const extension = url.pathname.split('.').pop();
//
//    
//
//     if(extensionsToCache.includes(extension)) {
//        
//         event.respondWith(
//             caches.open(CACHE_NAME)
//                 .then((cache) => {
//                     return cache.match(request)
//                         .then((response) => {
//                            
//                             if(response) {
//                                 console.log(url.pathname , "--service loaded from cached " , CACHE_NAME  ) ;
//                                 return response;
//                             }
//
//                             return fetch(request)
//                                 .then((networkResponse) => {
//                                     cache.put(request, networkResponse.clone());
//                                     console.log("--service cached " , CACHE_NAME , url.pathname) ;
//                                     return networkResponse;
//                                 })
//                                 .catch(() => caches.match('offline.html'));
//                         })
//                 })
//         );
//     }else{
//         console.log(url.pathname , "--service wont be cached (not matching!)" , CACHE_NAME  ) ;
//         event.respondWith(
//             caches.match(event.request)
//                 .then(() => {
//                     return fetch(event.request)
//                         .catch(() => caches.match('offline.html'))
//                 })
//         )
//     }
// });
//
// // Activate the SW 
// self.addEventListener('activate', (event) => {
//     console.log("--service active called" , CACHE_NAME);
//     const cacheWhitelist = [];
//     cacheWhitelist.push(CACHE_NAME);
//
//     event.waitUntil(
//         caches.keys().then((cacheNames) => Promise.all(
//             cacheNames.map((cacheName) => {
//                 if(!cacheWhitelist.includes(cacheName)) {
//                     return caches.delete(cacheName);
//                 }
//             })
//         ))
//
//     )
// });