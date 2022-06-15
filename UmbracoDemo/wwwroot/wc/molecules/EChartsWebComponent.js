import { Shadow } from "../Shadow.js";

export default class EChartsWebComponent extends Shadow() {
    static get echarts() {
        if (!EChartsWebComponent._echarts) {
            EChartsWebComponent._echarts = window.echarts
        }
        return EChartsWebComponent._echarts;
    }

    static set echarts(val) {
        EChartsWebComponent._echarts = val;
    }

    static get observedAttributes() {
        return ["style", "option"];
    }

    constructor(...args) {
        super(...args);

        this.root.innerHTML = `
            <div id="container" style="width: 100%; height: 100%;"></div>
        `;
    }

    connectedCallback() {
        if (!this.chart) {
            let container = this.root.querySelector("#container");
            this.chart = EChartsWebComponent.echarts.init(container);
            this.updateChart();
        }
    }

    disconnectedCallback() {
        let container = this.root.querySelector("#container");
        if (container) {
            container.innerHTML = "";
        }
        if (this.chart) {
            this.chart.dispose();
        }
        this.chart = null;
    }

    attributeChangedCallback(name, oldValue, newValue) {
        if (name === "option") {
            this.updateChart();
        } else if (name === "style") {
            let container = this.root.querySelector("#container");
            if (container) {
                container.style = newValue;
            }
            this.resizeChart();
        }
    }

    updateChart() {
        if (!this.chart) return;
        let option = JSON.parse(this.option || "{}");
        this.chart.setOption(option);;
    }

    resizeChart() {
        if (!this.chart) return;
        this.chart.resize();
    }

    get option() {
        if (this.hasAttribute("option")) {
            return this.getAttribute("option");
        } else {
            return "{}";
        }
    }

    set option(val) {
        if (val) {
            this.setAttribute("option", val);
        } else {
            this.setAttribute("option", "{}");
        }
        this.updateChart();
    }
}