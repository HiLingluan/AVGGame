#ifndef APPSFLYERUTIL_H
#define APPSFLYERUTIL_H

#import <Foundation/Foundation.h>



@interface AppsFlyerStat : NSObject
+(id)getInstance;

- (void)payFinish:(NSString *)paycode price:(NSString *)price goodid:(NSString *)goodidArg;

- (void)logEvent:(NSString *)eventName;

- (void)logEventWithJson:(NSString *)eventName json:(NSString*)jsonStr;

- (void)setRevenueReportOn:(int )state;

- (void)payBegin:(NSString *)paycode price:(NSString *)price;

- (void)payEventReport:(NSString *)eventNameArg paycode:(NSString *)paycode price:(NSString *)price;

@end


#endif /* APPSFLYERUTIL_H */
