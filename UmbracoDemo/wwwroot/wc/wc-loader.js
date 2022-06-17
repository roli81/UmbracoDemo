import GoogleMaps from './molecules/GoogleMaps.js';
import GMapsMarker from './molecules/GMapsMarker.js';
import EChartsWebComponent from './molecules/EChartsWebComponent.js';
import PaginatedTable from './molecules/PaginatedTable.js';

customElements.define('google-maps', GoogleMaps);
customElements.define('g-maps-marker', GMapsMarker);
customElements.define('e-chart', EChartsWebComponent);
customElements.define('paginated-table', PaginatedTable);