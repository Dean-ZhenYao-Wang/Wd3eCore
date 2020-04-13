var ganttData = {"data":[
        { text:"工位", parent:0, id:'1', open:true, progress:1 },
        { text:"工步", parent:1, id:'2', open:true, progress:1 },
        {"text":"齿轮箱上料","start_date":"02-01-1970 06:30","progress":1,"parent":2,"id":1379082074907,"end_date":"02-01-1970 8:00","owner":"1",section_id:20},
        {"text":"吊装收缩盘，安装滑环管","start_date":"02-01-1970 8:00","progress":0.5,"parent":1,"id":1379082074911,"end_date":"02-01-1970 9:30","owner":"1",section_id:20},
        {"text":"液压泵泵安装","start_date":"02-01-1970 9:00","progress":1,"parent":2,"id":1379082074912,"end_date":"02-01-1970 11:00","owner":"3"},
        {"text":"齿轮箱油泵接线","start_date":"02-01-1970 11:00","progress":1,"parent":2,"id":1379082074913,"end_date":"02-01-1970 12:00","owner":"2"},
        {"text":"齿轮箱段子盒","start_date":"02-01-1970 13:00","progress":1,"parent":1,"id":1379082074914,"end_date":"02-01-1970 15:00","owner":"1"},
        {"text":"刹车盘打扭矩","start_date":"02-01-1970 13:00","progress":1,"parent":1,"id":1379082074919,"end_date":"02-01-1970 15:00","owner":"3"},
        {"text":"齿轮箱座脚安装","start_date":"02-01-1970 14:00","progress":1,"parent":1,"id":1379082074935,"end_date":"02-01-1970 16:00","owner":"3"},
        {"text":"安装齿轮箱制动钳","start_date":"02-01-1970 15:00","progress":1,"parent":1,"id":1379082074910,"end_date":"02-01-1970 17:00","owner":"3"},
        {"text":"阀门、辅助电机，滑环支架安装","start_date":"02-01-1970 16:00","progress":1,"parent":1,"id":1379082073915,"end_date":"02-01-1970 18:00","owner":"3"},

        {"text":"发电机预装工位","progress":1,"parent":0,"id":1379339521965,"open":true},
        {"text":"发电机上料","start_date":"02-01-1970 07:00","duration":3,"progress":1,"parent":"1379339521965","id":1379339521969,"end_date":"02-01-1970 9:00","owner":"2"},
        {"text":"联轴器上料","start_date":"02-01-1970 07:00","duration":3,"progress":1,"parent":"1379339521965","id":1379339521970,"end_date":"02-01-1970 9:00","owner":"2"},
        {"text":"联轴器安装，涂防锈油","start_date":"02-01-1970 9:00","duration":2,"progress":1,"parent":"1379339521965","id":1379339521971,"end_date":"02-01-1970 12:00","owner":"3"},
        {"text":"安装蓝罩子，钢带制作","start_date":"02-01-1970 13:00","duration":7,"progress":1,"parent":"1379339521965","id":1379339521972,"end_date":"02-01-1970 15:00","owner":"4"},
        {"text":"安装线卡扣，发电机油泵及油管安装","start_date":"02-01-1970 15:00","duration":4,"progress":1,"parent":"1379339521965","id":1379339521973,"end_date":"02-01-1970 18:00","owner":"3"}]};

var ownerNames=[
    {key:"0", label: "无指定"},
    {key:"1", label: "Mark"},
    {key:"2", label: "John"},
    {key:"3", label: "Rebecca"},
    {key:"4", label: "Alex"}];
var treeGroup = [
    {
        key: -1, label: "一车间", open: true, children: [
            // {key: 1, label: "Elizabeth Taylor"},
            {
                key: -2, label: "车工班",open: true, children: [
                    {key: 1, label: "Mark"},
                    {key: 2, label: "John"}
                ]
            },
            {key: 3, label: "Rebecca"},
            {key: 4, label: "Alex"}
        ]
    },
    {
        key: -3, label: "二车间.", open: true, children: [
            {key: 5, label: "张三"},
            {key: 6, label: "李四"}
        ]
    }
];
