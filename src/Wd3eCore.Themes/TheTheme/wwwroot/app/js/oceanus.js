function createGantt(ganttData,ownerNames) {
    gantt.config.start_date = new Date(1970, 0, 2, 0, 0);
    gantt.config.end_date = new Date(1970, 0, 2, 24, 0);
    gantt.config.date_grid = "%H:%i";
    gantt.config.scale_unit = "hour";
    gantt.config.duration_unit = "minute";
    gantt.config.date_scale = "%H:%i";
    gantt.config.details_on_create = true;

    gantt.templates.task_class = function(start, end, obj){
        return "owner_"+obj.owner;
    }

    var textEditor = {type: "text", map_to: "text"};
    var startDateEditor = {type: "date", map_to: "start_date", min: new Date()};
    var durationEditor = {type: "number", map_to: "duration", min:0, max: 100};
    gantt.config.columns = [
        {name:"text",       label:"任务名称",  width:"*", tree:true, editor:textEditor },
        {name:"start_date", label:"开始时间", align: "center", width:50, editor: startDateEditor },
        {name:"end_date", label:"结束时间", align: "center", width:50 },
        {name:"duration", label:"时长", align: "center", width:50, editor: durationEditor },
        {name:"progress",   label:"资源",  template:function(obj){
                return gantt.getLabel("owner", obj.owner);
            }, align: "center", width:70 }
    ];
    gantt.config.grid_width = 450;

    gantt.attachEvent("onTaskCreated", function(obj){
        obj.duration = 4;
        obj.progress = 0.25;
    })

    gantt.templates.progress_text = function(start, end, task){
        return "<span>"+Math.round(task.progress*100)+ "% </span>";
    };

    gantt.locale.labels["section_owner"] = "Owner";
    gantt.config.lightbox.sections = [
        {name: "description", height: 38, map_to: "text", type: "textarea", focus: true},
        {name: "owner", height: 22, map_to: "owner", type: "select", options: ownerNames},
        {name: "time", type: "duration", map_to: "auto", time_format:["%d","%m","%Y","%H:%i"]}
    ];


    gantt.init("gantt_here");


    // modSampleHeight();
    gantt.parse(ganttData);
}
function createScheduler(ganttData,treeGroup) {
    var jsonData=[];
    ganttData2me();
    scheduler.locale.labels.timeline_tab = "Timeline"
    scheduler.locale.labels.section_custom = "Section";
    scheduler.config.xml_date = "%d-%m-%Y %H:%i";
    scheduler.config.first_hour = 0;
    scheduler.config.last_hour = 24;
    scheduler.config.readonly=true;
    scheduler.locale.labels.treetimeline_tab = "Tree";
    scheduler.createTimelineView({
        section_autoheight: false,
        name: "treetimeline",
        x_unit: "minute",
        x_date: "%H:%i",
        x_step: 60,
        x_size: 24,
        x_start: 0,
        x_length: 48,
        y_unit: treeGroup,
        y_property: "owner",
        render: "tree",
        folder_dy: 30,
        dy: 60
    });
    scheduler.init('scheduler_here', new Date(1970, 0, 2), "treetimeline");
    scheduler.parse(jsonData,"json");
    function ganttData2me(){
        for(i in ganttData.data){
            var task=ganttData.data[i];
            if(!task.owner)continue;
            jsonData.push(task);
        }
    }
    scheduler.attachEvent("onSchedulerResize",function () {
        conflictShow();
    });
    scheduler.attachEvent("onDataRender",function () {
        conflictShow();
    });

}
function conflictShow() {
    for(var i=0;i<$(".dhx_matrix_line").length;i++){
        if($(".dhx_matrix_line").eq(i).children("div.dhx_cal_event_line ").length>0){
            var temp=$(".dhx_matrix_line").eq(i).children("div.dhx_cal_event_line ").eq(0).css("top");
            for (var j=1;j<$(".dhx_matrix_line").eq(i).children("div.dhx_cal_event_line ").length;j++) {

                var forTemp=$(".dhx_matrix_line").eq(i).children("div.dhx_cal_event_line ").eq(j).css("top");
                if(forTemp!==temp){
                    $(".dhx_matrix_scell").eq(i).css({
                        backgroundColor:"#ff827f",
                        color:"#535353"
                    });
                    $(".dhx_data_table").eq(i).find("td").css({
                        backgroundColor:"#ff827f",
                        color:"#535353"
                    });
                    break;
                }
            }
        }
    }
}